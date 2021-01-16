# Rxjs

### Moving from 5 to 6

```typescript
// 5
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';

const sub = interval()
    .tap()
    .map()
    .subscribe()

// 6
import { Observable, Observer, Subject, Subscription, interval } from 'rxjs';
import { map, tap } from 'rxjs/operators';

const sub = interval()
    .pipe(
        tap(),
        map(),
        tap()
    )
    .subscribe()
```

### Basic Subscription

```typescript
// my-service.ts

import { Subject } from 'rxjs/Subject'; // Subject is like a public event emitter

@Injectible()
export class MyService {
    somethingChanged = new Subject<void>();
    somethingWithDefault = new BehaviourSubject('default value')

    updateSomething(){
        // change something and push event to subscribers
        this.somethingChanged.next()
    }
}
```

```typescript
// my-component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({})
export class MyComponent implements OnInit, OnDestroy {
    myService: MyService
    subscription: Subscription

    constructor(myService: MyService){}

    ngOnInit() {
        this.subscription = this.myService.somethingChanged.subscribe(
            () => {
                // react to something changing
            }
        )
    }

    ngOnDestroy() {
        // will pollute memory unless unsubscribed
        this.subscription.unsubscribe();
    }
}
```

### Observer Next, Error, Complete
An Observer can handle multiple events

```typescript
myObservable.subscribe(
    (next) => {
        // called when new value emitted
    },
    (error) => {
        // handle error
    },
    () => {
        // handle when observable finished
    }
)
```

this can also be written using an object
```typescript
oberver = {
    next: () => {},
    error: () => {},
    complete: () => {}
};
myObservable.subscribe(observer);
```

### Chaining Operators (Funel like)
You can chain operators inline to acheive more complicated logic

```typescript
myObservable
    .map(
        () => {} // transform the data
    )
    .throttleTimeout(1000) // drop frequent events
    .subscribe(
        () = {} // process event
    )
```

### Useful Operators

```typescript
//filter results
filter((value) => {
    return valur % 2 == 0 // return even numbers
})

// throttle events
throttleTimeout(1000) // 1 event per second emitted

// wait for inactivity before emitting
debounceTime(500) // emit after half second of no change

//dont emit duplicate values
map((event) => event.target.value) // every event is unique so use value
distinctUntilChanged() 

reduce((total, currentVal) => {}, 0) // apply a function to all elements and return total

scan((total, currentVal) => {}, 0) // same as above but return each intermiediate value (imagine long runing counter)

// pluck is cleaner than map for selecting properties
pluck('target', 'value')
// replaces...
map(event => event.target.value)

mergeMap() // see docs
// can take data from two observables and merge

```

