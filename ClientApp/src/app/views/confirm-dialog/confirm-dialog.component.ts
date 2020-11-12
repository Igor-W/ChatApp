import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit {
  private dialogTitle: string;
  private message: string;
  constructor(
    private dialogRef: MatDialogRef<ConfirmDialogComponent>, // pentru lucru cu popap curent
    @Inject(MAT_DIALOG_DATA) private data: { dialogTitle: string, message: string } // date, care transmitem in popap curent
  ) {
    this.dialogTitle = data.dialogTitle; // titlu
    this.message = data.message; // mesaj
  }

  ngOnInit() { }
  // apasam Ok
  private onConfirm(): void {
    this.dialogRef.close(true);
  }
  // apasam cancel
  private onCancel(): void {
    this.dialogRef.close(false);
  }
}
