# Routing

### Basic Setup
```typescript
// app.module.ts
import { RouterModule } from '@angular/router';

const routes = [
  { path: 'characters', component: TabsComponent, children: [
    // default is pathMatch=prefix which would catch any path
    // setting to full only matches when missing a side, 
    // which then redirects to side = all
    { path: '', redirectTo: 'all', pathMatch: 'full' },
    // path parameters 
    { path: ':side', component: ListComponent }
  ] },
  { path: 'new-character', component: CreateCharacterComponent },
  // catch all and redirect
  { path: '**', redirectTo: '/characters' }
];

@NgModule([
    imports: [
        RouterModule.forRoot(routes)
    ]
])
```

### Router Directives
Use `routerLink` to enable navigation

Use `routerLinkActive` to add a class when the applicable route is active, i.e. to highlight a button/tab

Use `routeLinkActiveOptions` to only detect active is the path matches exactly. This can fix issues when child paths exist.

```html
<div>
  <ul class="nav nav-tabs">
    <li role="presentation" 
        routerLinkActive="active">
        [routerLinkActiveOptions]="{exact: true}"
      <a routerLink="/characters/all">All</a>
      </li>
  </ul>
  <router-outlet></router-outlet>
</div>
```

### Reading Url Params
Use `ActivatedRoute` to get information about the current route. 

```typescript
import { ActivatedRoute } from '@angular/router';

@Component({})
export class ListComponent implements OnInit {
    constructor(
        activatedRoute: ActivatedRoute, 
        swService: StarWarsService) {
    this.activatedRoute = activatedRoute;
    this.swService = swService;
  }

  ngOnInit() {
    // activatedRoute is an observable
    this.activatedRoute.params.subscribe(
      (params) => {
        // everytime the url changes get/use the new params
        // here side was specifiec in routes with path = ':side'
        this.characters = this.swService.getCharacters(params.side)
      }
    );
  }
}
```

### Routing Module
It is better practise to refactor routing into its own module

```typescript
// app.module.ts
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    imports: [AppRoutingModule]
})
export class AppModule {}
```

```typescript
// app-routing.module.ts
import { RouterModule } from '@angular/router';

const routes = [{},{},{}]

@NgModule({
    imports: [RoutingModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
```

The same can be done for child routing modules using the `forChild()` method in place of `forRoot()`.