import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable()
export default class BaseAPIService {
  constructor(private http: HttpClient) { }

  getCall<T>(actionPath: string, paramters: any) {
    if (paramters == undefined)
      paramters = {};
    return this.http.get<T>(actionPath, { params: paramters });
  }

  deleteCall<T>(actionPath: string, paramters: any) {
    return this.http.delete<T>(actionPath, { params: paramters });
  }

  postCall<T>(actionPath: string, input: any) {
    return this.http.post<T>(actionPath, input);
  }
}
