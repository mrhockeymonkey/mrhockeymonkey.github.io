# Claude

```bash
# clear context
/clear

# clear context but replace with summaty
/compact

# go back to a point in time
ESC ESC

# enable auto-edit or thinking mode
SHIFT + TAB
```

```bash
export CLAUDE_CODE_USE_BEDROCK=1
export AWS_REGION=eu-west-1
export AWS_BEARER_TOKEN_BEDROCK=<token>

# Regional endpoints include a 10% pricing premium over global endpoints.
export ANTHROPIC_MODEL='global.anthropic.claude-opus-4-6-v1'
export ANTHROPIC_SMALL_FAST_MODEL='global.anthropic.claude-haiku-4-5-20251001-v1:0'

export ANTHROPIC_DEFAULT_HAIKU_MODEL='global.anthropic.claude-haiku-4-5-20251001-v1:0'
```
