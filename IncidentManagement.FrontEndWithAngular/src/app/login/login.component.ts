import { Component, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Login } from './login';
import { LoginService } from './login.service';
import { LoginConstants } from '../Common/constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: false
})
export class LoginComponent {

  constructor(private loginservice: LoginService) { }

  @Output() onLoginClick = new EventEmitter<string>();

  public loginForm = new FormGroup({
    userId: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(12),
      Validators.maxLength(20),
    ]),
  });

  public async login() {
    if (!this.loginForm.valid) {
      return;
    }
    var isLoggedIn = false;
    let loginModel = new Login();
    loginModel.userName = this.loginForm.value.userId as string;
    loginModel.password = this.loginForm.value.password as string;
    var token = '';
    var result = await this.loginservice.signIn(loginModel).subscribe(
      (response) => {
        token = response.jsonToken;
        localStorage.setItem(LoginConstants.jwtTokenKey, response.jsonToken);
        isLoggedIn = true;
      },
      (error) => {
        console.log(error.message);
        isLoggedIn = false;
      },
      () => {
        if (isLoggedIn) {
          this.onLoginClick.emit(this.loginForm.value.userId as string);
        }
      }
    );
  }
}
