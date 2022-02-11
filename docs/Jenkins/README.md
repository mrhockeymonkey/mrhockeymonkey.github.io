# Jenkins

## Useful Endpoints

`http://your-jenkins/env-vars.html` shows you all the vars that will be available at build time


## Custom Trigger

```groovy
import org.jenkinsci.plugins.pipeline.modeldefinition.Utils

/*
This pipeline takes a github credential (Username/Token) and list of configuired projects 
as shown below. You can configure jenkins to trigger this pipeline only on each commit and 
allow it to trigger other jenkins jobs based on changes detected.

triggerJobsPipeline {
    gitCredential = "github-credential"
    jobs = [
        'jenkins-job-1': [enabled: true, directory: "src/path1/"],
        'jenkins-job-2': [enabled: false, directory: "src/path2/"],
    ]
}
*/

def call(body) {
    def params = [:]
    body.resolveStrategy = Closure.DELEGATE_FIRST
    body.delegate = params
    body()

    def myProjects = params.jobs
    def myGitCredential = params.gitCredential
    def THIS_COMMIT = ""

    pipeline {
        agent {
            kubernetes {
                inheritFrom 'base'
            }
        }

        stages {
            stage ("trigger") {
                steps {
                    // there is a bug in jenkins whereby the GIT_* env vars can be missing or incorrect
                    // instead we will use git rev-parse
                    script {
                        THIS_COMMIT = sh(script: "git rev-parse HEAD", returnStdout: true).trim()
                        echo "rev-parse returned ${THIS_COMMIT}"
                    }
                    
                    echo """
                    THIS_COMMIT: ${THIS_COMMIT}
                    BRANCH_NAME: ${env.BRANCH_NAME}
                    """

                    // add target branch for comparison
                    withCredentials([
                        usernamePassword(credentialsId: myGitCredential, usernameVariable: 'GITHUB_USERNAME', passwordVariable: 'GIT_TOKEN')
                    ]) {
                        sh "git config http.sslVerify false;"
                        sh "git config url.\"https://api:${GIT_TOKEN}@git.uberit.net\".insteadOf \"https://git.uberit.net\""
                        sh 'git config --add remote.origin.fetch +refs/heads/master:refs/remotes/origin/master'
                        sh 'git fetch --no-tags'
                    }

                    script {
                        // discover changed files
                        def diffOutput
                        if (env.BRANCH_NAME == 'master') {
                            // for master detect change from previous commit
                            diffOutput = sh(script: "git diff --name-only HEAD~1...${THIS_COMMIT}", returnStdout: true)
                        } else {
                            // for PRs use three-dot comparison, i.e. all files changed since branching
                            diffOutput = sh(script: "git diff --name-only origin/master...${THIS_COMMIT}", returnStdout: true)
                        }
                        filesChanged = diffOutput.trim().split('\n').collect { it.trim() }.findAll { it != '' }
                        filesChanged.each{file ->
                            echo "changed: ${file}"
                        }

                        // trigger jobs according to changes
                        myProjects.each{entry -> 
                            def taskName = entry.key
                            def taskDir = entry.value.directory
                            def taskEnabled = entry.value.enabled
                            def taskParams = entry.value.parameters ?: []


                            if (directoryHasChanges(taskDir, filesChanged)) {
                                echo "changes found in ${taskDir}"
                                if (taskEnabled) {
                                    build(
                                        job: "Builds/${taskName}/${env.BRANCH_NAME}",
                                        parameters: taskParams,
                                        wait: false, 
                                        propagate: false
                                    ) 
                                } else {
                                    echo "${taskName} is not yet enabled"
                                }
                            } else {
                                echo "no changes found in ${taskDir}"
                            }
                        }
                    }
                }
            }
        }
    }
}

def directoryHasChanges(String dir, ArrayList filesChanged) {
    def result = false
    filesChanged.each{file ->
        if (file.startsWith(dir)){
            result = true
        }
    }
    return result
}
```
