import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookSearchService {
  readonly BaseURI = 'https://localhost:44373/api';

  constructor(private _http: HttpClient) { }

  search(term: string): Observable<any[]> {
    var ClientList = this._http.get(this.BaseURI + '?term=' + term)
      .pipe(
        map(
          (r: Response) => {
            return (r.json().then.length != 0 ? r.json() : [{ "ClientId": 0, "ClientName": "No Record Found" }]) as any[]
          }
        )
      );
    return ClientList;
  }  
}
