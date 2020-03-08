import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { PageLoaderInfo } from '../_notgenerated/helpers';

@Injectable({
  providedIn: 'root'
})
export class PageLoaderService {

    loaderInfo: Subject<PageLoaderInfo> = new Subject();

    show(message: string = null) {
        const msg = (!!message) ? message : '';
        const info = { loading: true, message: msg } as PageLoaderInfo;
        
        this.loaderInfo.next(info);
    }

    hide() {
        const info = { loading: false, message: 'Loading...' } as PageLoaderInfo;
        this.loaderInfo.next(info);
    }
}