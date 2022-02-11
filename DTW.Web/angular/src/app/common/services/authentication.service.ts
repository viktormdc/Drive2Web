import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {

    constructor(private http: HttpClient) {
    }


    login(username: string, password: string) {


      var url="https://localhost:44351/api/Admin/login";
        return this.http.post<any>(`${url}`, { username, password })
            .pipe(map(resposne => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(resposne));
              //  this.currentUserSubject.next(user);
                return resposne;
            }));
    }




    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
      //  this.currentUserSubject.next(null);
    }
}
