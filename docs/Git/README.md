# Git

```bash
# new worktree from master
git worktree add -b featureA ../featureA master

# alias
git config --global alias.wt '!f() { [ -z "$1" ] && { echo "usage: git wt <branch>"; exit 1; }; git worktree add -b "$1" "../$1" master; }; f'

# remove
git worktree remove featureA
```
