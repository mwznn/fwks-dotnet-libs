import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { AuthGuard } from './core/guards/auth/auth.guard'

const redirectRoute = 'home'

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: redirectRoute },
  { path: 'home', loadComponent: () => import('./pages/main').then(x => x.HomeComponent) },
  //{ path: 'protected', loadComponent: () => import('./pages/main').then(x => x.ProtectedComponent) },
  { path: 'protected', canActivate: [AuthGuard], loadComponent: () => import('./pages/main').then(x => x.ProtectedComponent) },
  { path: '**', pathMatch: 'full', redirectTo: redirectRoute }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

