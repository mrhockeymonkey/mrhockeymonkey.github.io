# Prometheus Notes

```yaml
- record: http_requests:rate
  expr: sum by (service, code) (rate(http_request_duration_seconds_count[5m]))
```
