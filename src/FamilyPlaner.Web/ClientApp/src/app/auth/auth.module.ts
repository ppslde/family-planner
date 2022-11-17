import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginDialogComponent } from './components/login-dialog/login-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FamilyMemberClient } from '@shared-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { UserInfoComponent } from './components/user-info/user-info.component';

@NgModule({
  declarations: [LoginDialogComponent, UserInfoComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    SharedModule,
  ],
  providers: [FamilyMemberClient],
  exports: [UserInfoComponent],
})
export class AuthModule {}
