import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { AuthGuard } from './core/guards/auth/auth.guard'

const mainPage = 'main'

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: mainPage },
    { path: mainPage, loadComponent: () => import('./pages').then((x) => x.MainComponent) },
    {
        path: 'protected',
        canActivate: [AuthGuard],
        loadComponent: () => import('./pages').then((x) => x.ProtectedComponent),
    },
    { path: '**', pathMatch: 'full', redirectTo: mainPage },
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
