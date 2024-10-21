import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
    return this.http.post<string>(`${this.baseUrl}/${encodingName}`, text);
  }
}
