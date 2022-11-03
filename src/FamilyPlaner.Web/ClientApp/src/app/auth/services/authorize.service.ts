import { EventEmitter, Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, of } from 'rxjs';
import {
  AuthenticateFamilyMemberCommand,
  FamilyMemberClient,
  UserInfoModel,
} from '@shared-module';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root',
})
export class AuthorizeService {
  constructor(
    private familyMemberClient: FamilyMemberClient,
    private jwtHelper: JwtHelperService
  ) {}

  AuthStateChanged: EventEmitter<boolean> = new EventEmitter<boolean>();

  logIn(userName: string, password: string): void {
    let data = new AuthenticateFamilyMemberCommand({
      userName: userName,
      password: password,
    });

    this.familyMemberClient.authenticate(data).subscribe({
      next: result => {
        if (
          result.accessToken &&
          result.userInfo &&
          !this.jwtHelper.isTokenExpired(result.accessToken)
        ) {
          this.storeUserData(result.accessToken, result.userInfo);
          this.AuthStateChanged.next(this.isTokenValid());
        } else {
          this.logOut();
        }
      },
      error: e => {
        this.logOut();
      },
    });
  }

  logOut(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    this.AuthStateChanged.next(this.isTokenValid());
  }

  isAuthenticated(): Observable<boolean> {
    return of(this.isTokenValid());
  }

  getUserInfo(): UserInfoModel | undefined {
    if (!this.isTokenValid()) return;

    const userdata = localStorage.getItem(USER_KEY);
    if (!userdata) return;

    return JSON.parse(userdata);
  }

  getUserToken(): string | undefined {
    if (!this.isTokenValid()) return undefined;

    return this.getToken();
  }

  private isTokenValid(): boolean {
    let token = this.getToken();
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  private getToken(): string | undefined {
    return localStorage.getItem(TOKEN_KEY) ?? undefined;
  }

  private storeUserData(token: string, userdata: UserInfoModel) {
    localStorage.setItem(TOKEN_KEY, token);
    localStorage.setItem(USER_KEY, JSON.stringify(userdata));
  }
}
