import { CommonModule } from '@angular/common'
import { Component, Inject } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { MatButtonModule } from '@angular/material/button'
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'

@Component({
    selector: 'app-data-dialog',
    standalone: true,
    imports: [CommonModule, FormsModule, MatDialogModule, MatInputModule, MatFormFieldModule, MatButtonModule],
    templateUrl: './data-dialog.component.html',
})
export class DataDialogComponent {
    lastName: string

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
        private dialogRef: MatDialogRef<DataDialogComponent>
    ) {
        this.lastName = ''
    }

    close(): void {
        this.dialogRef.close()
    }
}

export class DialogData {
    firstName: string = ''
}
