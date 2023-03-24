import { NgModule } from '@angular/core'
import { MatButtonModule } from '@angular/material/button'
import { MatIconModule } from '@angular/material/icon'
import { MatListModule } from '@angular/material/list'
import { MatMenuModule } from '@angular/material/menu'
import { MatProgressBarModule } from '@angular/material/progress-bar'
import { MatSidenavModule } from '@angular/material/sidenav'
import { MatToolbarModule } from '@angular/material/toolbar'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { KeycloakAngularModule } from 'keycloak-angular'
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { NavbarComponent } from '@app/core/components'

import _providers from './core/providers/_providers'

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserAnimationsModule,
        AppRoutingModule,
        KeycloakAngularModule,
        MatSidenavModule,
        MatListModule,
        MatMenuModule,
        MatButtonModule,
        MatIconModule,
        MatToolbarModule,
        MatProgressBarModule,
        NavbarComponent,
    ],
    providers: _providers,
    bootstrap: [AppComponent],
})
export class AppModule { }
