import { Router, NavigationEnd } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import {
  Component,
  Inject,
  ElementRef,
  OnInit,
  Renderer2,
  HostListener,
  OnDestroy,
} from '@angular/core';
import { ROUTES } from './sidebar-items';

// import { CUSTOMROUTES } from './custom-sidebar-items';
import { Module } from './models/module';
import { FeatureModuleService } from './service/featuremodule.service';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  public sidebarItems: any[];
  level1Menu = '';
  level2Menu = '';
  level3Menu = '';
  activeClass:boolean;
  public menuItems: any[];
  public innerHeight: any;
  public bodyTag: any;
  listMaxHeight: string;
  listMaxWidth: string;
  userFullName: string;
  module: Module[];
  userImg: string;
  userType: string;
  headerHeight = 60;
  currentRoute: string;
  routerObj = null;
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private renderer: Renderer2,
    public elementRef: ElementRef,
    private featureModuleService: FeatureModuleService,
    private authService: AuthService,
    private router: Router
  ) {
    const body = this.elementRef.nativeElement.closest('body');
    this.routerObj = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        // logic for select active menu in dropdown
        //const role = ['admin', 'teacher', 'student'];
        const currenturl = event.url.split('?')[0];
        //const firstString = currenturl.split('/').slice(1)[0];

        // if (role.indexOf(firstString) !== -1) {
        //   this.level1Menu = currenturl.split('/')[2];
        //   this.level2Menu = currenturl.split('/')[3];
        // } else {
          this.level1Menu = currenturl.split('/')[1];
          this.level2Menu = currenturl.split('/')[2];
        // }
        // close sidebar on mobile screen after menu select
        this.renderer.removeClass(this.document.body, 'overlay-open');
      }
    });
  }
  @HostListener('window:resize', ['$event'])
  windowResizecall(event) {
    this.setMenuHeight();
    this.checkStatuForResize(false);
  }
  @HostListener('document:mousedown', ['$event'])
  onGlobalClick(event): void {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.renderer.removeClass(this.document.body, 'overlay-open');
    }
  }
  callLevel1Toggle(event: any, element: any) {
    if (element === this.level1Menu) {
      this.level1Menu = '0';
    } else {
      this.level1Menu = element;
    }
    const hasClass = event.target.classList.contains('toggled');
    if (hasClass) {
      this.renderer.removeClass(event.target, 'toggled');
    } else {
      this.renderer.addClass(event.target, 'toggled');
    }
  }
  callLevel2Toggle(event: any, element: any) {
    if (element === this.level2Menu) {
      this.level2Menu = '0';
    } else {
      this.level2Menu = element;
    }
  }
  callLevel3Toggle(event: any, element: any) {
    if (element === this.level3Menu) {
      this.level3Menu = '0';
    } else {
      this.level3Menu = element;
    }
  }
  ngOnInit() {
    if (this.authService.currentUserValue) {
      const userRole = this.authService.currentUserValue.role;
      this.userFullName = this.authService.currentUserValue.username
      this.userImg = this.authService.currentUserValue.img;
      this.menuItems = this.module;
      this.sidebarItems = ROUTES.filter(
        (x) => x.role.indexOf(userRole) !== -1 || x.role.indexOf('All') !== -1
      );
      if (userRole === Role.StaffOfficer) {
        this.userType = Role.StaffOfficer;
      } else if (userRole === Role.Director) {
        this.userType = Role.Director;
      } else if (userRole === Role.ShipStaff) {
        this.userType = Role.ShipStaff;
      } else if (userRole === Role.CO) {
        this.userType = Role.CO;
      } else if (userRole === Role.DNWNEEOfficeStaff) {
        this.userType = Role.DNWNEEOfficeStaff;
      } else if (userRole === Role.LOEO) {
        this.userType = Role.LOEO;
      } else if (userRole === Role.SuperAdmin) {
        this.userType = Role.SuperAdmin;
      } else if (userRole === Role.MasterAdmin) {
        this.userType = Role.MasterAdmin;
      }else if (userRole === Role.EXO) {
        this.userType = Role.EXO;
      }else if (userRole === Role.DataEntry) {
        this.userType = Role.DataEntry;
      }else if (userRole === Role.ShipUser) {
        this.userType = Role.ShipUser;
      }else if (userRole === Role.AreaCommander) {
        this.userType = Role.AreaCommander;
      }else if (userRole === Role.DD) {
        this.userType = Role.DD;
      }
      else {
        this.userType = Role.MasterAdmin;
      }
    }

    this.initLeftSidebar();
    this.getfeaturemodules();
    this.bodyTag = this.document.body;
  }

  getfeaturemodules(){
    this.featureModuleService.getModuleFeatures().subscribe(res=>{
      this.module=res;   
    });
  }
  ngOnDestroy() {
    this.routerObj.unsubscribe();
  }
  initLeftSidebar() {
    const _this = this;
    // Set menu height
    _this.setMenuHeight();
    _this.checkStatuForResize(true);
  }
  setMenuHeight() {
    this.innerHeight = window.innerHeight;
    const height = this.innerHeight - this.headerHeight;
    this.listMaxHeight = height + '';
    this.listMaxWidth = '500px';
  }
  isOpen() {
    return this.bodyTag.classList.contains('overlay-open');
  }
  checkStatuForResize(firstTime) {
    if (window.innerWidth < 1170) {
      this.renderer.addClass(this.document.body, 'ls-closed');
    } else {
      this.renderer.removeClass(this.document.body, 'ls-closed');
    }
  }
  mouseHover(e) {
    const body = this.elementRef.nativeElement.closest('body');
    if (body.classList.contains('submenu-closed')) {
      this.renderer.addClass(this.document.body, 'side-closed-hover');
      this.renderer.removeClass(this.document.body, 'submenu-closed');
    }
  }
  mouseOut(e) {
    const body = this.elementRef.nativeElement.closest('body');
    if (body.classList.contains('side-closed-hover')) {
      this.renderer.removeClass(this.document.body, 'side-closed-hover');
      this.renderer.addClass(this.document.body, 'submenu-closed');
    }
  }
  logout() {
    this.authService.logout().subscribe((res) => {
      if (!res.success) {
        this.router.navigate(['/authentication/signin']);
      }
    });
  }
}
