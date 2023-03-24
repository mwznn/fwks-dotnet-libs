import { Component, OnInit } from '@angular/core'
import { AppContextService } from '@app/core/services'
import { environment } from '@app/environment'

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styles: [],
})
export class AppComponent implements OnInit {
    envName: string
    title: string
    showEnvironment: boolean

    constructor(
        public ctx: AppContextService) {
        this.title = 'Fwks UI'
        this.envName = environment.name
        this.showEnvironment = !environment.production
    }

    ngOnInit(): void {
        document.title = this.title
    }
}
