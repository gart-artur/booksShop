import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})

export class NotificationService {

  constructor(private toastr: ToastrService) {
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Success!', { enableHtml: true, onActivateTick: true, positionClass: 'toast-top-left' });
  }

  showError(message: string, timeout: number = 5000) {
    this.toastr.error(message, 'Error!', { enableHtml: true, onActivateTick: true, timeOut: timeout, positionClass: 'toast-top-left'});
  }

  showErrors(data: string[]) {
    if (data.length === 0) {
      return;
    }

    let errorMessage = '';

    data.forEach(file => {
      errorMessage = errorMessage.concat(file.concat('<br/>'));
    });

    this.toastr.error(errorMessage, 'Error!', { enableHtml: true, onActivateTick: true, positionClass: 'toast-top-left'});
  }

  showWarning(message: string, timeout: number = 5000) {
    this.toastr.warning(message, 'Warning!', { enableHtml: true, onActivateTick: true, timeOut: timeout, positionClass: 'toast-top-left'});
  }

  showInfo(message: string, timeout: number = 5000) {
    this.toastr.info(message, 'Attention!', {enableHtml: true, onActivateTick: true, timeOut: timeout});
  }
}
