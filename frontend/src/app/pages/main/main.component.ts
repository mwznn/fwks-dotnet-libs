import { CommonModule } from '@angular/common'
import { AfterViewInit, Component, ViewChild } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { MatButtonModule } from '@angular/material/button'
import { MatDialog, MatDialogModule } from '@angular/material/dialog'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatIconModule } from '@angular/material/icon'
import { MatInputModule } from '@angular/material/input'
import { MatTabsModule } from '@angular/material/tabs'
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar'
import { MatSort } from '@angular/material/sort'
import { MatTable, MatTableDataSource, MatTableModule } from '@angular/material/table'
import { MatGridListModule } from '@angular/material/grid-list'
import { ConfirmationDialogComponent, DataDialogComponent } from '@app/shared/components'
import { CustomerResponse } from '@app/core/models'
import { faker } from '@faker-js/faker'

@Component({
    selector: 'app-main',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        MatTabsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatTableModule,
        MatPaginatorModule,
        MatDialogModule,
        MatSnackBarModule,
        MatGridListModule,
    ],
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss'],
})
export class MainComponent implements AfterViewInit {
    @ViewChild(MatTable) table!: MatTable<CustomerResponse>
    @ViewChild(MatPaginator) paginator!: MatPaginator
    @ViewChild(MatSort) sort!: MatSort

    confirmationResult: boolean
    firstName: string
    lastName: string

    customers: CustomerResponse[]
    dataSource: MatTableDataSource<CustomerResponse>
    displayedColumns: string[]

    constructor(private dialog: MatDialog, private snackBar: MatSnackBar) {
        this.confirmationResult = false
        this.firstName = ''
        this.lastName = ''
        this.customers = []
        this.dataSource = new MatTableDataSource(this.customers)
        this.displayedColumns = ['guid', 'name', 'dateOfBirth', 'email', 'phoneNumber']
        for (let index = 0; index < 100; index++) {
            this.customers.push({
                guid: faker.datatype.uuid(),
                name: faker.name.fullName(),
                dateOfBirth: faker.date.birthdate().toLocaleDateString(),
                email: faker.internet.email(),
                phoneNumber: faker.phone.number('+### #########'),
            })
        }
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator
        this.dataSource.sort = this.sort
    }

    async openConfirmationDialog(): Promise<void> {
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { autoFocus: false })
        dialogRef
            .afterClosed()
            .toPromise()
            .then((confirmationResult) => (this.confirmationResult = confirmationResult))
    }

    async openDataDialog(): Promise<void> {
        const dialogRef = this.dialog.open(DataDialogComponent, {
            data: { firstName: this.firstName },
        })
        dialogRef
            .afterClosed()
            .toPromise()
            .then((lastName) => (this.lastName = lastName))
    }

    deleteRow(row: CustomerResponse): void {
        const index = this.customers.findIndex((x) => x.guid == row.guid)
        if (index == -1) return
        this.customers.splice(index, 1)
        this.updateDataSource()
        const snackBarRef = this.snackBar.open(`"${row.name}" was successfully removed.`, 'Undo', { duration: 3000 })
        snackBarRef
            .afterDismissed()
            .toPromise()
            .then((x) => {
                if (!x.dismissedByAction) return
                this.customers.splice(index, 0, row)
                this.updateDataSource()
            })
    }

    applyFilter(event: Event): void {
        this.dataSource.filter = (event.target as HTMLInputElement).value.trim().toLowerCase()
    }

    private updateDataSource(): void {
        this.dataSource.data = this.customers
        this.table.renderRows()
    }
}
