# Prometheus Notes

## Useful to troubleshoot

```plain
// check is a scrape was successful (1) or failed (0)
up{k8s.namespace.name="foo",service="bar"}

// how long a scrape took
scrape_duration_seconds{job="foo"}

// how many metrics a scrape recorded
scrape_samples_scraped{job="foo"}
```

## Federate Data
```plain
/federate?match[]={__name__!=""}
```

```yaml
honor_labels: true
honor_timestamps: true
params:
  match[]:
  - '{__name__!=""}'
scrape_interval: 1m
scrape_timeout: 10s
metrics_path: /federate
scheme: http
static_configs:
- targets:
  - my-prometheus.com
```

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

## Calculating Ratios
https://www.robustperception.io/using-group_left-to-calculate-label-proportions

```
cache_usage{event="HIT", source="ISomeClass"} 2868
cache_usage{event="MISS", source="ISomeClass"} 3036
cache_usage{event="REQUESTED", source="ISomeClass"} 5904

```

```
# ratio of cache hit to miss
cache_usage{event=~"HIT|MISS",  source="IClassifyServiceClient"} / 
ignoring(event) group_left sum without (event) (cache_usage{event="REQUESTED",source="IClassifyServiceClient"})
```




