import { Component, OnInit } from '@angular/core'
import { ContextService } from '@app/services'
import { environment } from '@env'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit {

  envName: string
  title: string
  showEnvironment: boolean

  constructor(
    public ctx: ContextService
  ) {
    this.title = 'Fwks UI'
    this.envName = environment.name
    this.showEnvironment = !environment.production
  }

  ngOnInit(): void {
    document.title = this.title
  }

}