import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { AuthorizeService } from '../../services/authorize.service';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss'],
})
export class LoginDialogComponent implements OnInit {
  constructor(
    private authSrv: AuthorizeService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<LoginDialogComponent>
  ) {
    this.loginform = this.fb.group({
      name: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  inProgess: boolean = false;
  loginform: FormGroup;

  ngOnInit(): void {
    this.authSrv.AuthStateChanged.subscribe(isAuthenticted => {
      this.inProgess = false;
      if (isAuthenticted) this.dialogRef.close();
    });
  }

  async saveForm() {
    if (this.loginform.invalid) return;

    this.inProgess = true;

    this.authSrv.logIn(
      this.loginform.get('name')?.value,
      this.loginform.get('password')?.value
    );
  }

  hasError(controlName: string, errorName: string): boolean {
    return this.loginform.controls[controlName].hasError(errorName);
  }
}
