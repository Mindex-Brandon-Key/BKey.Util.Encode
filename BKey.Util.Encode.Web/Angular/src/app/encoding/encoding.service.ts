import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EncodingService {
  private baseUrl = '/api/encoding';

  constructor(private http: HttpClient) { }

  getSupportedEncodings(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl);
  }

  encodeText(encodingName: string, text: string): Observable<string> {
    const headers = new HttpHeaders(
      {
        'Content-Type': 'application/x-www-form-urlencoded',
        'Accept': 'text/plain'
      });
    const body = new HttpParams().set('text', text);
    const path = `${this.baseUrl}/${encodingName}`
    return this.http.post(path, body.toString(), { headers, responseType: 'text' });
  }
}
