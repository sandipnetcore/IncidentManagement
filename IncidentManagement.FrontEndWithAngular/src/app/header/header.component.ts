import { Component, OnInit } from '@angular/core';
import { LoginConstants } from '../Common/constants';
import { jwtDecode } from 'jwt-decode';
import { UserClaims } from '../Common/user-claims';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  standalone: false
})
export class HeaderComponent implements OnInit {

  public isModalVisible = false;

  public componentName: string = '';
  public isLoggedIn: Boolean = false;
  public userName: string = '';


  ngOnInit() {
    var token = localStorage.getItem(LoginConstants.jwtTokenKey);
    if (token != null) {
      this.isLoggedIn = true;
    }
  }

  constructor(private router:Router) { }

  showModal(component: string) {
    this.isModalVisible = true;
    this.componentName = component;
  }

  hideModal(result:any) {
    this.isModalVisible = false;
    this.isLoggedIn = false;
    if (localStorage.getItem(LoginConstants.jwtTokenKey) != null) {
      this.isLoggedIn = true;
      var userInfo = JSON.stringify(this.extractClaims());
      localStorage.setItem(LoginConstants.UserKey, userInfo);
    }
  }

  
  public logout()
  {
    localStorage.removeItem(LoginConstants.jwtTokenKey);
    localStorage.removeItem(LoginConstants.UserKey);
    this.isLoggedIn = false;
    this.router.navigateByUrl('../../home');
  }

  private extractClaims(): UserClaims {
    var token = this.decodeToken();
    var claim = new UserClaims(token);
    this.userName = claim.LastName + ", " + claim.FirstName;
    return claim;
  }

  private decodeToken(): any {
    try {
      return jwtDecode(localStorage.getItem(LoginConstants.jwtTokenKey) || '');
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }
}
