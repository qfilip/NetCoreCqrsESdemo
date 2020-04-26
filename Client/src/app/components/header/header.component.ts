import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
    constructor(private router: Router) { }

    activeTab: number = 0;

    ngOnInit(): void { }

    navigateTo(route: string, activeTab: number) {
        this.activeTab = activeTab;
        const navigateTo = `/${route}`;
        this.router.navigate([navigateTo]);
    }
}