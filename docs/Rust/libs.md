# Useful Libs

- `nom` for parsing text
- `camino` for dealing with Utf Paths (`Utf8PathBuf`)
- `indexmap` for HashMap with preserved ordering
- `id_tree` for tree structures
- `color_eyre` for nicer error handling

```rust
fn main() -> color_eyre::Result<()>  {
    color_eyre::install().unwrap();
    do_something()? // shortcuts to error result
    Ok(())
}
```
