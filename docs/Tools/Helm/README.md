# Helm

## Join a list
```
{{/*
Join a list using ";"
*/}}
{{- define "my-chart.semicolonSeperatedList" -}}
{{- join ";" .Values.someList | quote }}
{{- end -}}
```

```yaml
# values.yaml
someList:
  - foo
  - bar
  - baz
  
# deployment.yaml

env:
  - name: MY_VARIABLE
    value: {{ template "my-chart.semicolonSeperatedList" . }}
```
