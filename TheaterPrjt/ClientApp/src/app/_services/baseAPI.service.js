"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BaseAPIService = void 0;
var BaseAPIService = /** @class */ (function () {
    function BaseAPIService(http) {
        this.http = http;
    }
    BaseAPIService.prototype.getCall = function (actionPath, paramters) {
        return this.http.get(actionPath, { params: paramters });
    };
    BaseAPIService.prototype.deleteCall = function (actionPath, paramters) {
        return this.http.delete(actionPath, { params: paramters });
    };
    BaseAPIService.prototype.postCall = function (actionPath, input) {
        return this.http.post(actionPath, input);
    };
    return BaseAPIService;
}());
exports.BaseAPIService = BaseAPIService;
//# sourceMappingURL=baseAPI.service.js.map