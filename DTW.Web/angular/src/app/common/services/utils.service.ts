import { HttpErrorResponse } from "@angular/common/http";
import { Injectable , EventEmitter} from "@angular/core";


@Injectable()
export class UtilsService {

  //#region Fields
  //#endregion

  //#region Constructor
  constructor() {

  }
  //#endregion

  //#region Public methods

  public parseErrors(errorResponse: HttpErrorResponse) {    
    var errorsToModel = JSON.parse(errorResponse.error.Value);    
    var validationErrors = this.validationErrors(errorsToModel);   
    if(validationErrors.SystemError !=undefined && validationErrors.SystemError !=null){      
      this.hasSystemError.emit(true);
    }
    return validationErrors;

  }
  //#endregion

  //#region Private methods

  private validationErrors(errors: any) {

    var model: { [k: string]: any } = {};
    errors.forEach((error: { Key: string | number; Message: any; }) => {
      model[error.Key] = error.Message;
    });

    return model;
  }

  //#endregion
  hasSystemError = new EventEmitter<boolean>();
}
