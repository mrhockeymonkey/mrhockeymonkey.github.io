# Prometheus Notes

```yaml
- record: http_requests:rate
  expr: sum by (service, code) (rate(http_request_duration_seconds_count[5m]))
  
- alert: HttpErrorCodeSLI
  expr: http_requests:rate{code=~"5.."} > 0
  for: 15m
  labels:
    severity: page
    applications: awacs
  annotations:
    summary: http error codes 5xx have been recorded for the past 15 minutes
```
