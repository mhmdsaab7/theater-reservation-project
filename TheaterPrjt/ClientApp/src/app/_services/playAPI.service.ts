import { OperationOutput, Play } from '../_models';
import BaseAPIService from './baseAPI.service';
import { Injectable } from '@angular/core';
@Injectable()
export class PlayAPIService {
  constructor(
    private baseAPIService: BaseAPIService
  ) {

  }

  public AddPlay(play: Play) {
    return this.baseAPIService.postCall<OperationOutput>("api/Play/AddPlay", play);
  }

  public UpdatePlay(play: Play) {
    return this.baseAPIService.postCall<OperationOutput>("api/Play/UpdatePlay", play);
  }

  public DeletePlay(playId: number) {
    return this.baseAPIService.deleteCall<OperationOutput>("api/Play/DeletePlay", { playId: playId });
  }
  public GetPlay(playId: number) {
    return this.baseAPIService.getCall("api/Play/DeletePlay", { playId: playId });
  }
  public GetFilteredPlays(theaterId: number, playName: string, effectiveTime?: Date) {
    var input = {
      TheaterId: theaterId, PlayName: playName, EffectiveTime: effectiveTime
    };
    return this.baseAPIService.postCall<Play[]>("api/Play/GetFilteredPlays", input);
  }
}
