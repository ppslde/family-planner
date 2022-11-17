import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog, MatLegacyDialogConfig as MatDialogConfig } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { IUserInfoModel, UserInfoModel } from '@shared-module';
import { AuthorizeService } from '../../services/authorize.service';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
})
export class UserInfoComponent implements OnInit {
  constructor(
    private dialog: MatDialog,
    private authService: AuthorizeService,
    private router: Router
  ) {}

  userInfo: IUserInfoModel | undefined;

  public get iconName(): string {
    return this.userInfo == undefined ? 'fingerprint' : 'logout';
  }

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    this.authService.AuthStateChanged.subscribe(
      isAuthenticted => {
        this.postAuthencationAction(isAuthenticted);
      },
      error => this.postAuthencationAction(false),
      () => this.postAuthencationAction(false)
    );
  }

  doAuthAction(action: string) {
    if (action === 'logout') {
      this.authService.logOut();
    } else {
      this.openDialog();
    }
  }

  private postAuthencationAction(isAuthenticted: boolean) {
    if (isAuthenticted) {
      this.userInfo = this.authService.getUserInfo();
      this.router.navigate(['/dashboard']);
    } else {
      this.userInfo = undefined;
      this.router.navigate(['/']);
    }
  }

  private openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;

    this.dialog.open(LoginDialogComponent, dialogConfig);
  }
}
