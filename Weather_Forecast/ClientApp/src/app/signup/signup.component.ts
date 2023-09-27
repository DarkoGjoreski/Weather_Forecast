import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/authService.service';
import { SHA256 } from 'crypto-js';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})

export class SignupComponent implements OnInit {

  signupForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router) {

    }

  ngOnInit(): void {
    this.signupForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  onSubmit() {
    if (this.signupForm.invalid) {
      return;
    }

    const email = SHA256(this.signupForm.value.email).toString();
    const password = SHA256(this.signupForm.value.password).toString();
    const firstName = this.signupForm.value.firstName;
    const lastName = this.signupForm.value.lastName;
    const user = { email, password, firstName, lastName };

    this.authService.signup(user).subscribe(
      (response: any) => {
        console.log('User registered successfully:', response);
        this.router.navigate(['/signin']);
      },
      (error: any) => {
        console.error('Error during registration:', error);
      }
    );
  }
}
