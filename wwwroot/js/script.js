// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    // Counter animation
    const counters = document.querySelectorAll('.counter');
    const speed = 200;
    
    counters.forEach(counter => {
        const target = +counter.getAttribute('data-target');
        const count = +counter.innerText;
        const increment = target / speed;
        
        if(count < target) {
            counter.innerText = Math.ceil(count + increment);
            setTimeout(updateCount, 1);
        } else {
            counter.innerText = target;
        }
        
        function updateCount() {
            const count = +counter.innerText;
            const increment = target / speed;
            
            if(count < target) {
                counter.innerText = Math.ceil(count + increment);
                setTimeout(updateCount, 1);
            } else {
                counter.innerText = target;
            }
        }
    });

    // Initialize Owl Carousel
    $(document).ready(function(){
        $('.partners-carousel').owlCarousel({
            loop: true,
            margin: 20,
            nav: true,
            dots: false,
            autoplay: true,
            autoplayTimeout: 3000,
            autoplayHoverPause: true,
            responsive: {
                0: { items: 1 },
                576: { items: 2 },
                992: { items: 3 },
                1200: { items: 4 }
            }
        });
        
        // Animate progress bars when scrolled to
        $(window).scroll(function() {
            $('.progress-bar').each(function() {
                const position = $(this).offset().top;
                const scroll = $(window).scrollTop();
                const windowHeight = $(window).height();
                if (scroll > position - windowHeight + 100) {
                    $(this).css('width', $(this).attr('aria-valuenow') + '%');
                }
            });
        }).scroll(); // Trigger on load
    });
});

// Optional JavaScript for enhanced interactivity
document.addEventListener('DOMContentLoaded', function() {
  // Add click event to logo cards
  const logoCards = document.querySelectorAll('.logo-card');
  
  logoCards.forEach(card => {
    card.addEventListener('click', function() {
      const companyName = this.querySelector('h5').textContent;
      console.log(`Selected partner: ${companyName}`);
      // You could trigger a modal or navigation here
    });
  });
  
  // Animate logos on scroll
  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.style.opacity = "1";
        entry.target.style.transform = "translateY(0)";
      }
    });
  }, { threshold: 0.1 });

  document.querySelectorAll('.logo-card').forEach(card => {
    card.style.opacity = "0";
    card.style.transform = "translateY(20px)";
    card.style.transition = "all 0.5s ease";
    observer.observe(card);
  });
});

// FOOTER //////

// Back to Top Button
document.addEventListener('DOMContentLoaded', function() {
  const backToTopButton = document.getElementById('backToTop');
  
  window.addEventListener('scroll', function() {
    if (window.pageYOffset > 300) {
      backToTopButton.classList.add('active');
    } else {
      backToTopButton.classList.remove('active');
    }
  });
  
  backToTopButton.addEventListener('click', function(e) {
    e.preventDefault();
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  });

  // Animate footer elements on scroll
  const footerElements = document.querySelectorAll('.footer-section [class*="col-"] > *');
  
  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry, index) => {
      if (entry.isIntersecting) {
        setTimeout(() => {
          entry.target.style.opacity = "1";
          entry.target.style.transform = "translateY(0)";
        }, index * 100);
      }
    });
  }, { threshold: 0.1 });

  footerElements.forEach(el => {
    el.style.opacity = "0";
    el.style.transform = "translateY(20px)";
    el.style.transition = "all 0.5s ease";
    observer.observe(el);
  });
});


// Simple horizontal scroll enhancement
        const highway = document.querySelector('.client-highway');
        let isDown = false;
        let startX;
        let scrollLeft;

        highway.addEventListener('mousedown', (e) => {
            isDown = true;
            startX = e.pageX - highway.offsetLeft;
            scrollLeft = highway.scrollLeft;
        });

        highway.addEventListener('mouseleave', () => {
            isDown = false;
        });

        highway.addEventListener('mouseup', () => {
            isDown = false;
        });

        highway.addEventListener('mousemove', (e) => {
            if(!isDown) return;
            e.preventDefault();
            const x = e.pageX - highway.offsetLeft;
            const walk = (x - startX) * 2;
            highway.scrollLeft = scrollLeft - walk;
        });







        /////////////BOOKINGS PAGE////////////////
document.addEventListener('DOMContentLoaded', function() {
  // Price data
  const spacePrices = {
    lagos: {
      "single-desk": 6000,
      "private-office": 10000
    },
    abuja: {
      "training-space": 15000,
      "hot-desk": 10000,
      "joint-desk": 3000
    }
  };

  // Space types
  const spaceTypes = {
    lagos: [
      { value: "single-desk", label: "Single Desk" },
      { value: "private-office", label: "Private Office" }
    ],
    abuja: [
      { value: "training-space", label: "Training Space" },
      { value: "hot-desk", label: "Hot Desk" },
      { value: "joint-desk", label: "Joint Desk" }
    ]
  };

  // Elements
  const locationSelect = document.getElementById('location');
  const spaceTypeSelect = document.getElementById('spaceType');
  const priceAmount = document.querySelector('.price-amount');
  const durationSelect = document.getElementById('duration');
  const bookingForm = document.getElementById('bookingForm');
  const userDetailsForm = document.getElementById('userDetailsForm');

  // Update space types when location changes
  locationSelect.addEventListener('change', function() {
    const location = this.value;
    
    // Enable and update space type dropdown
    spaceTypeSelect.disabled = !location;
    spaceTypeSelect.innerHTML = '<option value="">Select space type</option>';
    
    if (location) {
      spaceTypes[location].forEach(type => {
        const option = new Option(type.label, type.value);
        spaceTypeSelect.add(option);
      });
    }
    
    updatePrice();
  });

  // Update price when selections change
  spaceTypeSelect.addEventListener('change', updatePrice);
  durationSelect.addEventListener('change', updatePrice);

  function updatePrice() {
    const location = locationSelect.value;
    const spaceType = spaceTypeSelect.value;
    const duration = parseInt(durationSelect.value) || 1;
    
    if (location && spaceType) {
      const basePrice = spacePrices[location][spaceType];
      const totalPrice = basePrice * duration;
      priceAmount.textContent = `₦${totalPrice.toLocaleString()}`;
    } else {
      priceAmount.textContent = '₦0';
    }
  }

  // Form submission
  bookingForm.addEventListener('submit', function(e) {
    e.preventDefault();
    
    // Validate user details first
    if (!validateUserDetails()) {
      alert('Please complete your personal information first');
      return;
    }
    
    // Combine form data
    const userData = new FormData(userDetailsForm);
    const bookingData = new FormData(this);
    const combinedData = {
      ...Object.fromEntries(userData.entries()),
      ...Object.fromEntries(bookingData.entries())
    };
    
    console.log('Booking submission:', combinedData);
    alert(`Booking request received for ${combinedData.location}`);
    
    // Reset forms
    this.reset();
    userDetailsForm.reset();
    priceAmount.textContent = '₦0';
  });

  function validateUserDetails() {
    const requiredFields = ['fullName', 'email', 'phone'];
    return requiredFields.every(field => 
      document.getElementById(field).value.trim() !== ''
    );
  }

  // Set minimum date to today
  const today = new Date().toISOString().split('T')[0];
  document.getElementById('bookingDate').min = today;
});
        /////////////BOOKINGS PAGE//////////////////


/////////////SERVICES PAGE//////////////////
           // Toggle service content visibility with animation
        function toggleService(serviceNum) {
            const serviceContent = document.getElementById(`service-${serviceNum}`);
            const serviceHeader = document.querySelector(`#service-${serviceNum}`).previousElementSibling;
            
            // Toggle active class on header
            serviceHeader.classList.toggle('active');
            
            // Toggle content visibility with smooth animation
            if (serviceContent.classList.contains('active')) {
                serviceContent.style.maxHeight = serviceContent.scrollHeight + 'px';
                setTimeout(() => {
                    serviceContent.style.maxHeight = '0';
                }, 10);
                setTimeout(() => {
                    serviceContent.classList.remove('active');
                }, 400);
            } else {
                serviceContent.classList.add('active');
                serviceContent.style.maxHeight = serviceContent.scrollHeight + 'px';
                setTimeout(() => {
                    serviceContent.style.maxHeight = 'none';
                }, 400);
            }
            
            // Close other open services
            document.querySelectorAll('.service-content').forEach(content => {
                if (content.id !== `service-${serviceNum}` && content.classList.contains('active')) {
                    content.style.maxHeight = content.scrollHeight + 'px';
                    setTimeout(() => {
                        content.style.maxHeight = '0';
                    }, 10);
                    setTimeout(() => {
                        content.classList.remove('active');
                        content.previousElementSibling.classList.remove('active');
                    }, 400);
                }
            });
        }
        
        // Open first service by default
        document.addEventListener('DOMContentLoaded', function() {
            setTimeout(() => {
                toggleService(1);
            }, 500);
        });



        