# Calling APIs

For newer versions of angular (version 4.3.x or above) http package is included under common
```typescript
// for app.module.ts
import { HttpClientModule } from '@angular/common/http'
@NgModule({
    import: [
        BrowserModule
        HttpClientModule // must be after browser module
    ]
})

// for service.ts
import { HttpClient } from '@angular/common/http'
constructor(private httpClient: HttpClient) { }
// Remove any map(res => res.json()) calls (which we'll add later in our code). They are no longer needed.
```

For older versions of Angular you need to install the `http` package
```typescript
//npm install --save @angular/http
import { HttpModule } from ''

// for service.ts
import { Http } from '@angular/http'
```

