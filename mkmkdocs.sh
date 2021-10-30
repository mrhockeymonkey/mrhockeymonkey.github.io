#!/bin/bash

# dynamically build nav section
# recursively searchs folders looking for README.md before stopping
# note: subdirs will be ignored if the parent dir does not have at least one article

function FindDirs {
    local DIR=$1
    local DIR_NAME=$(basename $1)
    echo $(find $DIR -maxdepth 1 -type d -not \( -name $DIR_NAME \))
}

function AddMarkdownToNav {
    local DIR=$1
    FILES=$(find $DIR -type f -name '*.md')

    for F in $FILES; do
        echo $F
    done
}

function AppendToNav {
    DIR=$1
    DIR_NAME=$(basename $1)
    local INDENT_1=$(($2+2))
    local INDENT_2=$(($2+4))
    DIRS=$(FindDirs $DIR)
    MARKDOWN_FILES=$(find $DIR -maxdepth 1 -type f -name '*.md')

    # if there are no markdown files there is no point including this dir
    if [ -z "$MARKDOWN_FILES" ]; then
        echo "[WARN] No markdown or dirs found in $DIR"
        return 0
    fi

    # add this folder
    echo "$(printf '%*s' "$INDENT_1")- $(basename $DIR_NAME):" >> mkdocs.yml

    # add each markdown
    for M in $MARKDOWN_FILES; do
        VAL=$(echo $M | sed 's/.\/docs\///')
        echo "$(printf '%*s' "$INDENT_2")- $(basename $M .md): \"$VAL\"" >> mkdocs.yml
    done

    # recursively add subfolders
    for D in $DIRS; do
        AppendToNav $D $INDENT_1 
    done
}

cp mkdocs.template.yml mkdocs.yml
for TOP_LEVEL_DIR in $(FindDirs ./docs); do
    AppendToNav $TOP_LEVEL_DIR 0
done
