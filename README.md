# FLIPZON - Professional E-Commerce Application

Welcome to FLIPZON, an advanced E-Commerce application developed using .NET MAUI. Inspired by the best features of Amazon and Flipkart, FLIPZON brings a seamless shopping experience to users. Leveraging the power of .NET 7, Prism Framework, and MVVM design pattern, FLIPZON integrates various UI controls, custom renders, and third-party libraries for a robust and user-friendly application.


## Features

- **Authentication:**
  - Secure and seamless login and signup screens.

- **Product Discovery:**
  - Intuitive home screen showcasing featured products.
  - Products screen with pagination for easy navigation.

- **Product Details:**
  - Detailed product information with a dedicated product details screen.

- **Search Functionality:**
  - Efficient search screen for finding desired products.

- **Shopping Cart:**
  - User-friendly cart screen for managing selected items.

- **Order Placement:**
  - Streamlined order placement process.

- **User Profile:**
  - Personalized profile screen for user customization.

- **Address Management:**
  - Address list screen and an option to add new addresses.

## Screens

- Login Screen
- SignUp Screen
- Home Screen
- Product Details Screen
- Products Screen
- Search Screen
- Cart Screen
- Address List Screen
- Add Address Screen
- Profile Screen
- Order Confirmation Screen



## Screenshots



| ![Login Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/login.png) | ![SignUp Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/signup.png) | ![Home Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/home.png) |
|:---:|:---:|:---:|
| Login Screen | SignUp Screen | Home Screen |

| ![Product Details Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/details.png) | ![Products Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/products.png) | ![Search Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/search.png) |
|:---:|:---:|:---:|
| Product Details Screen | Products Screen | Search Screen |

| ![Cart Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/cart.png) | ![Address List Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/addressList.png) | ![Add Address Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/addAddress.png) |
|:---:|:---:|:---:|
| Cart Screen | Address List Screen | Add Address Screen |


| ![Profile Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/profile.png) | ![Order Confirmation Screen](https://github.com/learnToEarnWithSrikanth/FlipZon/blob/main/FlipZon/FlipZon/Screenshots/orderConfirmation.png) |
|:---:|:---:|
| Profile Screen | Order Confirmation Screen 


## Video Link

Check out our video tutorial for a quick overview of the project:

[![Project Video](https://img.youtube.com/vi/YOUTUBE_VIDEO_ID/0.jpg)](https://www.youtube.com/watch?v=5ePrzhA6QPI)

## UI Controls 

- Collection View
- Carousel View
- Swipe View
- Forms with Validation
- Custom Controls
- Custom Renders
- Pagination for products

## Tech Stack 

- **.NET MAUI:**
  - Cutting-edge framework for cross-platform app development.

- **Prism Framework:**
  - Implementing MVVM design pattern for a modular and maintainable codebase.

- **API Integration:**
  - Seamless integration with external APIs for up-to-date product information.

- **Database Integration:**
  - Utilizing SQLite PCL for efficient and reliable data storage.

## 3rd Party Libraries

1. **User Dialogs:**
   - For Alerts and confirmations Popups.

2. **Mopups:**
   - To display popups.

3. **Community Toolkit:**
   - For Form Validations and Converters

4. **SQLite PCL:**
   - For Database support

## Challenges Faced and Solutions

1. Getting the Error Database Connection Failed while using SQL lite DataBase
-  This is the wiered error while using the prism framework with .net 7 ,solved this issue by Unistalling other packages Related sqllite and installed only SqlLitePCl

2. UserDialogs Loading indicators are working when using them in onAppearing
- As per the documentation the UserDialogs will not work in the onAppearing or Constructor, hence created the Custom Loading Indicator

3. Some of the 3rd party libraries are incompactable with .net 7 and Prism
- When I am trying use  3rd party libraries like UserDialogs ,Mopups, Community Toolkit it says the library is incompactable, the solution is install the dowmgrade version instead of latest.

## Getting Started 

1. Clone the repository.
2. Ensure .NET 7 is installed.
3. Restore NuGet packages.
4. Build and run the application.

Feel free to explore and enhance FLIPZON for an even more incredible shopping experience!

**Happy Shopping with FLIPZON!**
