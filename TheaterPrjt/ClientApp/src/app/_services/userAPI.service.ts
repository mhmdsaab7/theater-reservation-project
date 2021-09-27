import { User } from '../_models';
import BaseAPIService from './baseAPI.service';

import { Injectable } from '@angular/core';
@Injectable()
export class UserAPIService {
  constructor(
    private baseAPIService: BaseAPIService
  ) {

  }
  public Authenticate(username: string, password: string) {
    var input = { Username: username, Password: password }
    return this.baseAPIService.postCall<User>("api/User/Authenticate", input);
  }
}
