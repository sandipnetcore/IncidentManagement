import { TestBed } from '@angular/core/testing';

import { LoginService } from './login.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { NgModule } from '@angular/core';
import { Login } from './login';
import { EndPointAddress } from '../Common/end-point-address';

describe('LoginService', () => {
  let service: LoginService;
  let httpClientTesting: HttpTestingController;
  let userCred: Login;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        LoginService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ]
    });

    service = TestBed.inject(LoginService);
    httpClientTesting = TestBed.inject(HttpTestingController);

    userCred = new Login();

    userCred.userName = 'Test';
    userCred.password = 'Test';
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should be able to recieve response from signin', () => {

    const token = "1233456";

    service.signIn(userCred).subscribe((response: string) => {
      expect(response).toBeTruthy();
      expect(response).toEqual(token);
    });

    const requestedUrl = httpClientTesting.expectOne(EndPointAddress.Login_API);
    expect(requestedUrl.request.method).toEqual('POST');
    requestedUrl.flush(token);

  });
});
