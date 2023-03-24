import { CommonModule } from '@angular/common'
import { Component, Input } from '@angular/core'
import { MatButtonModule } from '@angular/material/button'
import { MatIconModule } from '@angular/material/icon'
import { MatMenuModule } from '@angular/material/menu'
import { MatDrawer } from '@angular/material/sidenav'
import { RouterModule } from '@angular/router'
import { AppContextService } from '@app/core/services'
import { environment } from '@app/environment'

@Component({
    selector: 'app-navbar',
    standalone: true,
    imports: [
        CommonModule,
        RouterModule,
        MatMenuModule,
        MatButtonModule,
        MatIconModule
    ],
    templateUrl: './navbar.component.html',
})
export class NavbarComponent {
    environment: string
    title: string

    @Input() drawer!: MatDrawer
    @Input() class?: string
    @Input() color: string
    @Input() sidenav: boolean

    constructor(
        public context: AppContextService) {
        this.title = 'Fwks UI'
        this.environment = environment.name
        this.color = 'primary'
        this.sidenav = false
    }
}
