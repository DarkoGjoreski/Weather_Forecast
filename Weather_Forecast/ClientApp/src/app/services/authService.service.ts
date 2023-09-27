import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44364/user';

  constructor(private http: HttpClient) {}

  signup(user: any): Observable<any> {
    const signupUrl = `${this.apiUrl}/signup`;
    return this.http.post(signupUrl, user);
  }

  signin(user: any): Observable<any> {
    const signinUrl = `${this.apiUrl}/signin`;
    return this.http.post(signinUrl, user,{responseType: 'text'});
  }
}
