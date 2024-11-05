html++ {
  head {
    title("Intergalactic Coding Academy")
    style {
      body { font-family: 'Consolas', sans-serif; background-color: #0d1117; color: dodgerblue; }
      .cosmic-wrapper { max-width: 1200px; margin: 0 auto; padding: 20px; }
      .space-themed { background-color: #161b22; padding: 20px; border-radius: 10px; }
      .star-navigation ul { list-style-type: none; padding: 0; }
      .star-navigation li { display: inline; margin-right: 20px; }
      .star-navigation a { color: #58a6ff; text-decoration: none; }
      .wormhole-btn { background-color: #238636; color: white; border: none; padding: 10px 20px; border-radius: 5px; cursor: pointer; }
      .course-grid { display: grid; grid-template-columns: repeat(3, 1fr); gap: 20px; }
      .course-card { background-color: #21262d; padding: 15px; border-radius: 8px; }
      .testimonial { font-style: italic; margin: 20px 0; }
      .footer { text-align: center; padding: 20px 0; }
    }
  }
  body {
    div(id="main-container", class="cosmic-wrapper") {
      header(id="galactic-header", class="space-themed") {
        h1("Welcome to the Intergalactic Coding Academy")
        nav(class="star-navigation") {
          ul {
            li { a(href="#home", "Mission Control") }
            li { a(href="#courses", "Nebula of Knowledge") }
            li { a(href="#about", "Celestial Origins") }
            li { a(href="#contact", "Quantum Communication") }
          }
        }
      }
      section(id="hero-section", class="supernova-splash") {
        h2("Embark on a Cosmic Coding Journey")
        p("Discover the secrets of the digital universe and become a master of the coding cosmos!")
        button(class="wormhole-btn", onclick="alert('Your journey begins now!')", "Begin Your Odyssey")
      }
      section(id="courses", class="space-themed") {
        h2("Our Stellar Courses")
        div(class="course-grid") {
          div(class="course-card") {
            h3("Quantum Algorithm Basics")
            p("Master the fundamentals of quantum computing and algorithm design.")
            button(class="wormhole-btn", "Enroll Now")
          }
          div(class="course-card") {
            h3("Intergalactic Web Development")
            p("Learn to create websites that span across galaxies.")
            button(class="wormhole-btn", "Enroll Now")
          }
          div(class="course-card") {
            h3("AI for Space Exploration")
            p("Harness the power of AI to navigate the cosmos.")
            button(class="wormhole-btn", "Enroll Now")
          }
        }
      }
      section(id="testimonials", class="space-themed") {
        h2("Voices from the Cosmos")
        div(class="testimonial") {
          p("The Intergalactic Coding Academy transformed my career. I now work as a lead developer on Mars!")
          p("- Zorg, Graduate of 2150")
        }
        div(class="testimonial") {
          p("Thanks to the skills I learned here, I've developed a universal translator for all known alien languages.")
          p("- Lyra, Class of 2149")
        }
      }
      footer(class="space-themed") {
        p("© 2150 Intergalactic Coding Academy. All rights reserved across the multiverse.")
        div(class="social-links") {
          a(href="/hello", "HoloBook")
          a(href="#fadsfsdf", "QuantumTweet")
          a(href="#dfasfd", "GalacticGram")
        }
      }
    }
  }
  script {
    "
    console.log('Welcome to the Intergalactic Coding Academy!');
    function toggleDarkMode() {
      document.body.classList.toggle('dark-mode');
    }
    function enrollCourse(courseName) {
      alert('You have successfully enrolled in ' + courseName + '!');
    }
    document.querySelectorAll('.course-card .wormhole-btn').forEach(button => {
      button.addEventListener('click', function() {
        enrollCourse(this.parentNode.querySelector('h3').textContent);
      });
    });
    "
  }
}