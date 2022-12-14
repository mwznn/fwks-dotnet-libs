import { CommonModule } from '@angular/common'
import { HttpClient, HttpClientModule } from '@angular/common/http'
import { Component } from '@angular/core'
import { ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup } from '@angular/forms'
import { MatButtonModule } from '@angular/material/button'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatIconModule } from '@angular/material/icon'
import { MatInputModule } from '@angular/material/input'
import { MatListModule } from '@angular/material/list'
import { IsStatusCodeErrorPipe } from '@app/pipes'
import { ContextService } from '@app/services'
import { environment } from '@env'
import { HttpErrorResponse, Page } from 'src/app/domain/common'
import { CustomerResponse } from 'src/app/domain/responses'

@Component({
  selector: 'app-protected',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    IsStatusCodeErrorPipe
  ],
  templateUrl: './protected.component.html',
  styles: []
})
export class ProtectedComponent {

  customers: CustomerResponse[]
  form: UntypedFormGroup

  res: HttpErrorResponse | null

  constructor(
    fb: UntypedFormBuilder,
    public ctx: ContextService,
    private http: HttpClient
  ) {
    this.customers = []
    this.res = null
    this.form = fb.group({
      name: ['']
    })
  }

  search(): void {
    const queryParam = this.form.controls.name.value == '' ? '' : `?name=${this.form.controls.name.value}`
    this.http.get<Page<CustomerResponse>>(`${environment.endpoints.api}/v1/customers${queryParam}`).toPromise()
      .then(res => {
        this.res = null
        this.customers = res.items
      })
      .catch(res => {
        this.res = HttpErrorResponse.set(res)
        this.customers = []
      })
  }
}