import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/authService.service';
import { SHA256 } from 'crypto-js';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  signinForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router) {

  }
  ngOnInit(): void {
    this.signinForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.signinForm.invalid) {
      return;
    }

    const email = SHA256(this.signinForm.value.email).toString();
    const password = SHA256(this.signinForm.value.password).toString();
    const user = { email, password };

    this.authService.signin(user).subscribe(
      (response: any) => {
        console.log('User signed in successfully:', response);
        localStorage.setItem('AccessToken', response);
        this.router.navigate(['/weather']);
      },
      (error: any) => {
        console.error('Error during sign in:', error);
      }
    );
  }
}
