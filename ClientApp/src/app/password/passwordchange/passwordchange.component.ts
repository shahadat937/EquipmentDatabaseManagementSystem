import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/service/auth.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { UserService } from 'src/app/security/service/User.service';

@Component({
  selector: 'app-passwordchange',
  templateUrl: './passwordchange.component.html',
  styleUrls: ['./passwordchange.component.sass']
})
export class PasswordchangeComponent implements OnInit {

  submitted = false;
  loading = false;
  error = '';
  hide1 = true;
  hide2 = true;
  hide3 = true;
  userId: any;
  buttonText: string;
  pageTitle: string;
  destination: string;
  PasswordUpdateForm: FormGroup;
  validationErrors: string[] = [];
  role: any;
  traineeId: any;
  subscription: any;

  constructor(
    private snackBar: MatSnackBar, 
    private authService: AuthService,
    private fb: FormBuilder, 
    private router: Router,  
    private route: ActivatedRoute,
    private confirmService: ConfirmService, 
    private userService: UserService
  ) {}

  @ViewChild('labelImport') labelImport: ElementRef;

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.userId = this.authService.currentUserValue.id;
    this.traineeId = this.authService.currentUserValue.traineeId.trim();
    
    this.pageTitle = 'Password Change';
    this.destination = 'Add';
    this.buttonText = "Save";
    
    this.initializeForm();
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  initializeForm() {
    this.PasswordUpdateForm = this.fb.group({
      traineeId: [0],
      userId: [''],
      currentPassword: ['', [
        Validators.required, 
        Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'), 
        Validators.maxLength(20)
      ]],
      newPassword: ['', [
        Validators.required, 
        Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'), 
        Validators.maxLength(20)
      ]],
      confirmPassword: ['', [
        Validators.required, 
        this.matchValues('newPassword')
      ]]
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : { isMatching: true };
    };
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  onSubmit() {
    const id = this.userId;
    this.PasswordUpdateForm.get('userId').setValue(this.userId);

    this.subscription = this.confirmService.confirm('Confirm Update message', 'Are You Sure Update Item').subscribe(result => {
      if (result) {
        this.loading = true;
        this.subscription = this.userService.updatePassword(this.PasswordUpdateForm.value).subscribe(response => {
          console.log(response);
          this.router.navigateByUrl('/password/password-change');
          this.reloadCurrentRoute();
          this.snackBar.open('Information Updated Successfully', '', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-success'
          });
        }, error => {
          this.validationErrors = error;
        });
      
        this.loading = false;}
    });
  }

  get isPasswordsMatching() {
    return this.PasswordUpdateForm.hasError('isMatching', ['confirmPassword']);
  }
}
