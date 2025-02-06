import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from '../../../../src/app/core/models/role';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { MatSnackBar } from '@angular/material/snack-bar';
import { isNull } from '@angular/compiler/src/output/output_ast';
@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent
  extends UnsubscribeOnDestroyAdapter
  implements OnInit
{
  authForm: FormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;

  schoolId:any;
  instructorId:any;
  traineeId:any;
  number1 : number;
  number2 : number;
  captchaSum : number;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,private snackBar: MatSnackBar
  ) {
    super();
  }

  ngOnInit() {
    this.generateCaptcha();
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      captchaAnswer : ['', Validators.required],
    });
    this.schoolId=20;
  }
  get f() {
    return this.authForm.controls;
  }
  // adminSet() {
  //   this.authForm.get('username').setValue('admin@school.org');
  //   this.authForm.get('password').setValue('admin@123');
  // }
  // teacherSet() {
  //   this.authForm.get('username').setValue('teacher@school.org');
  //   this.authForm.get('password').setValue('teacher@123');
  // }
  // studentSet() {
  //   this.authForm.get('username').setValue('student@school.org');
  //   this.authForm.get('password').setValue('student@123');
  // }

  generateCaptcha(){
    this.number1 = Math.floor(Math.random() *10 );
    this.number2 = Math.floor(Math.random() *20 );
    this.captchaSum = this.number1 + this.number2;
  }

  onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.error = '';
    if (this.authForm.invalid) {

      this.snackBar.open('Email and Password not valid !', '', {
        duration: 2000,
        verticalPosition: 'bottom',
        horizontalPosition: 'right',
        panelClass: 'snackbar-danger'
      });
     
      return;
    } else {
      this.subs.sink = this.authService
        .login(this.f.email.value, this.f.password.value, this.f.captchaAnswer.value, this.captchaSum)
        .subscribe(
          (res) => {
            if (res) {
              this.snackBar.open('login successfull.', '', {
                duration: 3000,
                verticalPosition: 'bottom',
                horizontalPosition: 'right',
                panelClass: 'snackbar-success'
              });
             

  
             // setTimeout(() => {
              const roleCheck = this.authService.currentUserValue.role;







              // if( isNull(roleCheck)){

              // }
              const role = this.authService.currentUserValue.role.trim();
              const traineeId =  this.authService.currentUserValue.traineeId.trim();
              const branchId =  this.authService.currentUserValue.branchId  ? this.authService.currentUserValue.branchId.trim() : "";

           
                if (role === Role.All || role === Role.MasterAdmin || role === Role.Director || role === Role.DNWNEEOfficeStaff || role === Role.StaffOfficer  || role === Role.DD ||  role === Role.AreaCommander || role === Role.SOL || role === Role.SOR || role === Role.OffceStaffDNWEE || role === Role.CSO || role === Role.FLO || role ===Role.FLOStaff  ) {
                  
                  this.router.navigate(['/admin/dashboard/main']);
                }else if (role === Role.ShipUser || role === Role.CO || role === Role.EXO || role === Role.ShipStaff || role === Role.LOEO || role === Role.LOEOWTR) {
                  this.router.navigate(['/admin/dashboard/ship-dashboard']);
                }else if (role === Role.DataEntry) {
                  this.router.navigate(['/admin/dashboard/school-dashboard']);
                } else {
                  this.router.navigate(['/authentication/signin']);
                }
                this.loading = false;
            //  }, 1000);
            } else {
              this.error = 'Invalid Login';
            }
          },
          (error) => {
            this.error = error;
            this.submitted = false;
            this.loading = false;
          }
        );
    }
  }
}
