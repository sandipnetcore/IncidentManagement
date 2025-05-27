import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EndPointAddress } from '../Common/end-point-address';
import { Login } from './login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }

  public loginStatus: string = '';

  public signIn(model: Login):Observable<any> {
    //const httpOptions = {
    //  headers: new HttpHeaders({
    //    'Content-Type': 'application/json',
    //    Authorization: 'my-auth-token'
    //  })
    //};
    return this.httpClient.post<any>(EndPointAddress.Login_API, model);
  }
}
