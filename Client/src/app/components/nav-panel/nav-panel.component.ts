import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'nav-panel',
    templateUrl: './nav-panel.component.html',
    styleUrls: ['./nav-panel.component.scss']
})
export class NavPanelComponent implements OnInit {
    constructor(private router: Router) { }

    @Output('onClicked') onClicked: EventEmitter<string> = new EventEmitter(null);

    ngOnInit(): void { }

    navigate(route: string) {
        const currentRoute = route.charAt(0).toUpperCase() + route.slice(1);
        this.onClicked.emit(currentRoute);

        const navigateTo = `/${route}`;
        this.router.navigate([navigateTo]);
    }

}
