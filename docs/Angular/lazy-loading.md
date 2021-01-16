# Lazy Loading

```typescript
// app-routing.module.ts
import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
  },
  {
    path: 'workout/:workoutId',
    loadChildren: () => import('./workout-session/workout-session.module').then( m => m.WorkoutSessionPageModule)
  }
];
@NgModule({
  imports: [
    // preloadingStrategy helps to stop Navigation latency due to having to load the module on demmand. 
    // PreloadAllModules loads all lazyloading routes in the background???
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
```

```typescript
// workout-session-routing.module.ts
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { WorkoutSessionPage } from './workout-session.page';

const routes: Routes = [
  {
    path: '',
    component: WorkoutSessionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WorkoutSessionPageRoutingModule {}
```