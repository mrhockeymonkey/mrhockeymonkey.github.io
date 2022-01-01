#!/bin/bash
IFS=';'

function FindDirs {
    local DIR=$1
    local DIR_NAME=$(basename $1)
    local DIRS=$(find $DIR -maxdepth 1 -type d -not \( -name $DIR_NAME \) -printf "%p;")
    echo "$DIRS"
}

function AppendToNav {
    DIR=$1
    DIR_NAME=$(basename $1)
    local INDENT_1=$(($2+2))
    local INDENT_2=$(($2+4))
    MARKDOWN_FILES=$(find $DIR -maxdepth 1 -type f -name '*.md' -printf "%p;")

    # if there are no markdown files there is no point including this dir
    if [ -z "$MARKDOWN_FILES" ]; then
        echo "[INFO] No markdown found in $DIR, moving on"
        return 0
    else 
        # add this folder
        echo "$(printf '%*s' "$INDENT_1")- $(basename $DIR_NAME):" >> mkdocs.yml

        # add each markdown
        for M in $MARKDOWN_FILES; do
            VAL=$(echo $M | sed 's/.\/docs\///')
            echo "Adding $M"
            echo "$(printf '%*s' "$INDENT_2")- $(basename $M .md): \"$VAL\"" >> mkdocs.yml
        done
    fi

    # recursively add subfolders
    DIRS=$(FindDirs $DIR)
    echo $DIRS
    if [ -z "$DIRS" ]; then
        echo "[INFO] No dirs found in $DIR, moving on"
    else
        for D in $DIRS; do
            echo "Processing $D"
            AppendToNav $D $INDENT_1 
        done
    fi
}

MAIN_DIRS="C#;\
Powershell;\
Theory"

cp mkdocs.template.yml mkdocs.yml
for MAIN_SECTION in $MAIN_DIRS; do
    AppendToNav "./docs/$MAIN_SECTION" 0
done

# add misc section

MISC_DIRS="Bash;\
Docker;\
Angular;\
Coffee;\
DIY;\
Kerberos;\
Prometheus;\
Helm"

echo "$(printf '%*s' "2")- $(basename "Etc"):" >> mkdocs.yml
for MISC_SECTION in $MISC_DIRS; do
    AppendToNav "./docs/$MISC_SECTION" 2
done