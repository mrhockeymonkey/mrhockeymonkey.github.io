# Sigma + Graphology JS in Blazor

To include npm packages in blazor
```bash
npm init -y
npm install webpack webpack-cli -save-dev
npm install sigma graphology graphology-layout-force
```
add webpack command to package.json under scripts
```json
"build": "webpack ./index.js --output-path ../wwwroot/js --output-filename index.bundle.js --mode=development"
```

```xml
  <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
    <Exec Command="npm run build" WorkingDirectory="Npm" />
  </Target>
```

Note: for some reason dotnet watch breaks the graph container rendering.