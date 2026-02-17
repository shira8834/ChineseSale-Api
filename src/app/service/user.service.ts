import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../environment/environment';
import { AddUserDto, AuthResponse, LogInUserDto } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {

   private http = inject(HttpClient);
  private apiUrl = `${environment.serverUrl}/api/Auth`;

    registerUser(user: AddUserDto): Observable<AddUserDto> {
    return this.http.post<AddUserDto>(`${this.apiUrl}/register`, user);
      }

    LogInUser(user: LogInUserDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.apiUrl + '/login', user);
      }
}