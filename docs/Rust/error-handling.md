# Error Handling

One approch is to allow any type of `Error`

```rs
fn main() -> Result<(), Box<dyn std::error::Error>> {
    let some_var = env::var("SOME_ENV_VAR")?;
} 
```

Or you can define all possible erros your application could encounter

```rs
#[derive(Debug)]
enum AppError {
    MissingEnvVar(String),
    // etc
}

impl std::error::Error for AppError {}

impl Display for AppError {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        match self {
            AppError::MissingEnvVar(name) => write!(f, "Missing environment variable '{}'!", name)
        }
    }
}

// note that errors returned from main will use Debug to print output, not Display
fn main() -> Result<(), AppError> {
    let some_var = env::var("SOME_ENV_VAR")
        .map_err(|e| AppError::MissingEnvVar("SOME_ENV_VAR".to_string()))?;
}
```
