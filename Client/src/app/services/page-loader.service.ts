import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { PageLoaderInfo } from '../_notgenerated/helpers';

@Injectable({
  providedIn: 'root'
})
export class PageLoaderService {

    private loaderInfo$: Subject<PageLoaderInfo> = new BehaviorSubject(null);

    show(message: string = null) {
        const msg = (!!message) ? message : 'default message';
        const info = { loading: true, message: msg } as PageLoaderInfo;
        
        this.loaderInfo$.next(info);
    }

    hide() {
        const info = { loading: false, message: 'Loading...' } as PageLoaderInfo;
        this.loaderInfo$.next(info);
    }

    get pageLoaderState() {
        return this.loaderInfo$.asObservable();
    }
}