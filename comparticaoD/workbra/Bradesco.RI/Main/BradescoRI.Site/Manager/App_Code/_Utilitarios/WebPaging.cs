using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFoundations.ServerControls
{
    #region "Enumerators"
    [Flags()]
    public enum NavigationOption
    {
        None = 0,
        FirstAndLastAlways = 1,
        PreviousAndNextAlways = 2,
        Etc = 4,
        FirstAndLastNeededOnly = 8,
        PreviousAndNextNeededOnly = 16,
        AllAlways = FirstAndLastAlways | PreviousAndNextAlways | Etc,
        AllNeededOnly = FirstAndLastNeededOnly | PreviousAndNextNeededOnly | Etc
    }

    [Flags()]
    public enum SeparatorOption
    {
        None = 0,
        PageNumbers = 1,
        PreviousAndNext = 2,
        FirstAndLast = 4,
        Etc = 8,
        Arrows = PreviousAndNext | FirstAndLast,
        Numbers = PageNumbers | Etc,
        All = Arrows | Numbers
    }

    public enum PagingRootElement
    {
        Div,
        P,
        Ul
    }

    #endregion

    [ToolboxData("<{0}:WebPaging runat=server />")]
    public class Paginador : WebControl
    {
        [Serializable()]
        public struct WebPagingState
        {
            public object BaseState { get; set; }
            public int CurrentPageNumber { get; set; }
            public int PageSize { get; set; }
            public int MaxVisiblePages { get; set; }
            public IEnumerable DataSource { get; set; }
        }

        #region "Constants"
        private const string CssSelected = "selected";
        private const string defaultTextNext = ">";
        private const string defaultTextPrevious = "<";
        private const string defaultTextFirst = "<<";
        #endregion
        private const string defaultTextLast = ">>";

        #region "Method SaveViewState"
        protected override object SaveViewState()
        {
            WebPagingState result = new WebPagingState
            {
                BaseState = base.SaveViewState(),
                CurrentPageNumber = this.CurrentPageNumber,
                PageSize = this.PageSize,
                MaxVisiblePages = this.MaxVisiblePages,
                DataSource = this.DataSource
            };

            return result;
        }
        #endregion

        #region "Method LoadControlState"
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null && savedState is WebPagingState)
            {
                WebPagingState state = (WebPagingState)savedState;
                this.m_currentPageNumber = state.CurrentPageNumber;
                this.m_pageSize = state.PageSize;
                this.m_maxVisiblePages = state.MaxVisiblePages;
                this.m_dataSource = state.DataSource;
                this.OnPropertyChanged();
                base.LoadViewState(state.BaseState);
            }
        }
        #endregion

        #region "Event PageChanged"
        public event EventHandler PageChanged;

        protected virtual void OnPageChange()
        {
            if (PageChanged != null)
            {
                PageChanged(this, new EventArgs());
            }
        }
        #endregion

        #region "EventHandler PageLink_Click"
        private void PageLink_Click(object sender, EventArgs e)
        {
            int p = 0;
            LinkButton b = sender as LinkButton;
            if (Int32.TryParse(b.Text, out p))
            {
                this.CurrentPageNumber = p;
                this.OnPageChange();
            }
        }
        #endregion

        #region "EventHandler lnkFirst_Click"
        private void lnkFirst_Click(object sender, EventArgs e)
        {
            if (this.PageCount > 0)
            {
                this.CurrentPageNumber = 1;
                this.OnPageChange();
            }
        }
        #endregion

        #region "EventHandler lnkLast_Click"
        private void lnkLast_Click(object sender, EventArgs e)
        {
            if (this.PageCount > 0)
            {
                this.CurrentPageNumber = this.PageCount;
                this.OnPageChange();
            }
        }
        #endregion

        #region "EventHandler lnkNext_Click"
        private void lnkNext_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageNumber < this.PageCount)
            {
                this.CurrentPageNumber += 1;
                this.OnPageChange();
            }
        }
        #endregion

        #region "EventHandler lnkPrevious_Click"
        private void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageNumber > 1)
            {
                this.CurrentPageNumber -= 1;
                this.OnPageChange();
            }
        }
        #endregion

        #region "Fields"
        private LinkButton[] pageMenuCircular;
        private int startPage;
        private int endPage;
        private LinkButton lnkFirst;
        private LinkButton lnkLast;
        private LinkButton lnkPrevious;
        private LinkButton lnkNext;
        #endregion
        private Label etc;

        #region "--- CSS Properties ---"

        #region "Property CssClassPageSelected"
        private string m_cssClassPageSelected = CssSelected;
        public string CssClassPageSelected
        {
            get { return this.m_cssClassPageSelected; }
            set { this.m_cssClassPageSelected = value; }
        }
        #endregion

        #region "Property CssClassPageLink"
        private string m_cssClassPageLink = string.Empty;
        public string CssClassPageLink
        {
            get { return this.m_cssClassPageLink; }
            set { this.m_cssClassPageLink = value; }
        }
        #endregion

        #region "Property CssClassEtc"
        private string m_cssClassEtc = string.Empty;
        public string CssClassEtc
        {
            get { return m_cssClassEtc; }
            set { m_cssClassEtc = value; }
        }
        #endregion

        #region "Property CssClassFirst"
        private string m_cssClassFirst = string.Empty;
        public string CssClassFirst
        {
            get { return m_cssClassFirst; }
            set { m_cssClassFirst = value; }
        }
        #endregion

        #region "Property CssClassPrevious"
        private string m_cssClassPrevious = string.Empty;
        public string CssClassPrevious
        {
            get { return m_cssClassPrevious; }
            set { m_cssClassPrevious = value; }
        }
        #endregion

        #region "Property CssClassNext"
        private string m_cssClassNext = string.Empty;
        public string CssClassNext
        {
            get { return m_cssClassNext; }
            set { m_cssClassNext = value; }
        }
        #endregion

        #region "Property CssClassLast"
        private string m_cssClassLast = string.Empty;
        public string CssClassLast
        {
            get { return m_cssClassLast; }
            set { m_cssClassLast = value; }
        }
        #endregion

        #endregion

        #region "--- Text Properties ---"

        #region "Property TextNext"
        private string m_textNext = defaultTextNext;
        [Localizable(true)]
        public string TextNext
        {
            get { return this.m_textNext; }
            set { this.m_textNext = value; }
        }
        #endregion

        #region "Property TextPrevious"
        private string m_textPrevious = defaultTextPrevious;
        [Localizable(true)]
        public string TextPrevious
        {
            get { return this.m_textPrevious; }
            set { this.m_textPrevious = value; }
        }
        #endregion

        #region "Property TextFirst"
        private string m_textFirst = defaultTextFirst;
        [Localizable(true)]
        public string TextFirst
        {
            get { return this.m_textFirst; }
            set { this.m_textFirst = value; }
        }
        #endregion

        #region "Property TextLast"
        private string m_textLast = defaultTextLast;
        [Localizable(true)]
        public string TextLast
        {
            get { return this.m_textLast; }
            set { this.m_textLast = value; }
        }
        #endregion

        #endregion

        #region "Property RootElement"
        private PagingRootElement m_rootElement = PagingRootElement.Div;
        public PagingRootElement RootElement
        {
            get { return m_rootElement; }
            set { m_rootElement = value; }
        }
        #endregion

        #region "Property NavigationOptions"
        private NavigationOption m_navigationOptions = NavigationOption.AllAlways;
        public NavigationOption NavigationOptions
        {
            get { return m_navigationOptions; }
            set { m_navigationOptions = value; }
        }
        #endregion

        #region "Property SeparatorOptions"
        private SeparatorOption m_separatorOptions = SeparatorOption.All;
        public SeparatorOption SeparatorOptions
        {
            get { return m_separatorOptions; }
            set { m_separatorOptions = value; }
        }
        #endregion

        #region "Property CurrentPageNumber"
        private int m_currentPageNumber = 1;
        /// <summary>
        /// Gets or sets the current page number. 
        /// The default page number is 1.
        /// </summary>
        public int CurrentPageNumber
        {
            get { return this.m_currentPageNumber; }
            set
            {
                if (this.m_currentPageNumber != value && value > 0)
                {
                    this.m_currentPageNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region "Property PageSize"
        private int m_pageSize;
        /// <summary>
        /// Gets or sets the number of items to display on a single page.
        /// </summary>
        public int PageSize
        {
            get { return this.m_pageSize; }
            set
            {
                if (this.m_pageSize != value)
                {
                    this.m_pageSize = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region "Property DataSource"
        private IEnumerable m_dataSource;
        /// <summary>
        /// Gets or sets the datasource to be paged.
        /// </summary>
        public IEnumerable DataSource
        {
            get { return this.m_dataSource; }
            set
            {
                if ((!object.ReferenceEquals(this.m_dataSource, value)))
                {
                    this.m_dataSource = value;
                    this.ResetCurrentPageNumber();
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region "Property PageDataItems"
        private object m_pageDataItems;
        /// <summary>
        /// Gets the datasource for the requested page.
        /// </summary>
        public object PageDataItems
        {
            get { return this.m_pageDataItems; }
        }
        #endregion

        #region "Property PageNumbersSeparator"
        private string m_pageNumbersSeparator = " | ";
        /// <summary>
        /// Gets or sets the separator between page MenuCircular. 
        /// The default separator is " | ".
        /// </summary>
        public string PageNumbersSeparator
        {
            get { return this.m_pageNumbersSeparator; }
            set { this.m_pageNumbersSeparator = value; }
        }
        #endregion

        #region "Property PageCount"
        private int m_pageCount;
        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int PageCount
        {
            get { return this.m_pageCount; }
        }
        #endregion

        #region "Property MaxVisiblePages"
        /// <summary>
        /// Gets or sets the maximum number of page MenuCircular to show at a time.
        /// </summary>
        private int m_maxVisiblePages = -1;
        public int MaxVisiblePages
        {
            get { return this.m_maxVisiblePages; }
            set
            {
                if (value >= -1 && this.m_maxVisiblePages != value)
                {
                    this.m_maxVisiblePages = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region "Method ResetCurrentPage"
        private void ResetCurrentPageNumber()
        {
            this.CurrentPageNumber = 1;
        }
        #endregion

        #region "Method CreatePageMenuCircular"
        /// <summary>
        /// Create the LinkButtons to navigate to the pages of data.
        /// </summary>
        private void CreatePageMenuCircular()
        {
            int i = 0;
            int p = 0;
            LinkButton lb = default(LinkButton);
            this.Controls.Clear();

            this.pageMenuCircular = new LinkButton[this.m_pageCount];
            for (i = 0; i <= this.m_pageCount - 1; i++)
            {
                lb = new LinkButton();
                this.pageMenuCircular[i] = lb;
                p = (i + 1);
                lb.Text = p.ToString();
                lb.ID = string.Format("{0}_lnkPageLink_{1}", this.ID, p);
                lb.CausesValidation = false;
                lb.Click += PageLink_Click;
                this.Controls.Add(lb);
            }

            this.lnkFirst = new LinkButton();
            this.lnkFirst.Text = this.TextFirst;
            this.lnkFirst.ID = string.Format("{0}_lnkFirstPage", this.ID);
            this.lnkFirst.CssClass = this.CssClassFirst;
            this.lnkFirst.CausesValidation = false;
            lnkFirst.Click += lnkFirst_Click;
            this.Controls.Add(lnkFirst);

            this.lnkLast = new LinkButton();
            this.lnkLast.Text = this.TextLast;
            this.lnkLast.ID = string.Format("{0}_lnkLastPage", this.ID);
            this.lnkLast.CssClass = this.CssClassLast;
            this.lnkLast.CausesValidation = false;
            lnkLast.Click += lnkLast_Click;
            this.Controls.Add(lnkLast);

            this.lnkPrevious = new LinkButton();
            this.lnkPrevious.Text = this.TextPrevious;
            this.lnkPrevious.ID = string.Format("{0}_lnkPreviousPage", this.ID);
            this.lnkPrevious.CssClass = this.CssClassPrevious;
            this.lnkPrevious.CausesValidation = false;
            lnkPrevious.Click += lnkPrevious_Click;
            this.Controls.Add(lnkPrevious);

            this.lnkNext = new LinkButton();
            this.lnkNext.Text = this.TextNext;
            this.lnkNext.ID = string.Format("{0}_lnkNextPage", this.ID);
            this.lnkNext.CssClass = this.CssClassNext;
            this.lnkNext.CausesValidation = false;
            lnkNext.Click += lnkNext_Click;
            this.Controls.Add(lnkNext);

            if (this.CurrentPageNumber == 1)
            {
                this.lnkFirst.Enabled = false;
                this.lnkPrevious.Enabled = false;
            }

            if (this.CurrentPageNumber == this.m_pageCount)
            {
                this.lnkLast.Enabled = false;
                this.lnkNext.Enabled = false;
            }

            if (this.m_maxVisiblePages < 0 || this.m_maxVisiblePages >= this.m_pageCount)
            {
                //Show all page MenuCircular
                this.startPage = 1;
                this.endPage = this.m_pageCount;
            }
            else if (this.m_pageCount > this.m_maxVisiblePages)
            {
                int distance = 0;
                distance = this.m_maxVisiblePages / 2;
                this.endPage = this.CurrentPageNumber + distance;

                if (this.endPage > this.m_pageCount)
                {
                    this.endPage = this.m_pageCount;
                }

                this.startPage = this.endPage - this.m_maxVisiblePages + 1;
                if (this.startPage < 1)
                {
                    this.startPage = 1;
                    this.endPage = this.startPage + this.m_maxVisiblePages - 1;

                    if (this.endPage > this.m_pageCount)
                    {
                        this.endPage = this.m_pageCount;
                    }
                }
            }

        }
        #endregion

        #region "Method OnPropertyChanged"
        /// <summary>
        /// Set the parameters for the PagedDataSource and create the page MenuCircular.
        /// </summary>
        protected virtual void OnPropertyChanged()
        {
            if (this.DataSource != null)
            {
                //Verifica a quantidade de registros que a página deve ter
                if (this.PageSize > 0)
                {
                    PagedDataSource pagedDataSource = default(PagedDataSource);
                    if (this.m_pageDataItems == null || this.m_pageDataItems.GetType() != typeof(PagedDataSource))
                    {
                        pagedDataSource = new PagedDataSource();
                        pagedDataSource.AllowPaging = true;
                        this.m_pageDataItems = pagedDataSource;
                    }
                    else
                    {
                        pagedDataSource = (PagedDataSource)this.m_pageDataItems;
                    }
                    pagedDataSource.CurrentPageIndex = this.CurrentPageNumber - 1;
                    pagedDataSource.DataSource = this.DataSource;
                    pagedDataSource.PageSize = this.PageSize;
                    this.m_pageCount = pagedDataSource.PageCount;

                    this.CreatePageMenuCircular();
                }
                else
                {
                    //O data source não será paginado se PageSize <= 0
                    this.m_pageDataItems = this.DataSource;
                }
            }
        }
        #endregion

        #region "Method Render"
        /// <summary>
        /// Render the control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.etc = new Label();
            etc.Text = "...";
            if (this.CssClassEtc != null && this.CssClassEtc.Length > 0)
            {
                etc.CssClass = this.CssClassEtc;
            }

            if (this.DesignMode)
            {
            }
            else
            {
                if (this.Visible && this.DataSource != null && this.PageSize > 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
                    if (this.CssClass != null && this.CssClass.Length > 0)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
                    }
                    switch (this.RootElement)
                    {
                        case PagingRootElement.Div:
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                        case PagingRootElement.P:
                            writer.RenderBeginTag(HtmlTextWriterTag.P);
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                        case PagingRootElement.Ul:
                            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                    }
                    int i = 0;

                    //Writes the "First" link button
                    if (NavigationOptionContains(NavigationOption.FirstAndLastAlways) || (this.CurrentPageNumber > 1 && NavigationOptionContains(NavigationOption.FirstAndLastNeededOnly)))
                    {
                        WriteNavigationLink(writer, this.lnkFirst, false, SeparatorOptionContains(SeparatorOption.FirstAndLast));
                    }

                    //Writes the "Previous" link button
                    if (NavigationOptionContains(NavigationOption.PreviousAndNextAlways) || (this.CurrentPageNumber > 1 && NavigationOptionContains(NavigationOption.PreviousAndNextNeededOnly)))
                    {
                        WriteNavigationLink(writer, this.lnkPrevious, false, SeparatorOptionContains(SeparatorOption.PreviousAndNext));
                    }

                    //Writes the begining etc visual aid
                    if (this.startPage > 1 && NavigationOptionContains(NavigationOption.Etc))
                    {
                        WriteNavigationLink(writer, etc, false, SeparatorOptionContains(SeparatorOption.Etc));
                    }

                    //Writes the page numbers
                    if (this.m_maxVisiblePages != 0)
                    {
                        for (i = startPage - 1; i <= this.endPage - 2; i++)
                        {
                            WriteNavigationLink(writer, WritePageNumber(i), false, SeparatorOptionContains(SeparatorOption.PageNumbers));
                        }
                        WriteNavigationLink(writer, WritePageNumber(i), false, false);
                    }

                    //Writes the ending etc visual aid
                    if (this.endPage < this.m_pageCount && NavigationOptionContains(NavigationOption.Etc))
                    {
                        WriteNavigationLink(writer, etc, SeparatorOptionContains(SeparatorOption.Etc), false);
                    }

                    //Writes the "Next" link button
                    if (NavigationOptionContains(NavigationOption.PreviousAndNextAlways) || (this.CurrentPageNumber < this.m_pageCount && NavigationOptionContains(NavigationOption.PreviousAndNextNeededOnly)))
                    {
                        WriteNavigationLink(writer, this.lnkNext, SeparatorOptionContains(SeparatorOption.PreviousAndNext), false);
                    }

                    if (NavigationOptionContains(NavigationOption.FirstAndLastAlways) || (this.CurrentPageNumber < this.m_pageCount && NavigationOptionContains(NavigationOption.FirstAndLastNeededOnly)))
                    {
                        WriteNavigationLink(writer, this.lnkLast, SeparatorOptionContains(SeparatorOption.FirstAndLast), false);
                    }

                    writer.RenderEndTag();
                }
            }
        }
        #endregion

        #region "Method WriteNavigationLink"
        private void WriteNavigationLink(HtmlTextWriter writer, WebControl obj, bool separatorBefore, bool separatorAfter)
        {
            if (this.RootElement == PagingRootElement.Ul)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, obj.CssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                obj.CssClass = null;
            }

            if (separatorBefore)
            {
                writer.Write(Page.Server.HtmlEncode(this.m_pageNumbersSeparator));
            }
            obj.RenderControl(writer);
            if (separatorAfter)
            {
                writer.Write(Page.Server.HtmlEncode(this.m_pageNumbersSeparator));
            }

            if (this.RootElement == PagingRootElement.Ul)
            {
                writer.RenderEndTag();
            }
        }
        #endregion

        #region "Method WritePageNumber"
        private LinkButton WritePageNumber(int index)
        {
            LinkButton lb = default(LinkButton);
            LinkButton lb2 = default(LinkButton);

            if (!this.DesignMode)
            {
                lb = pageMenuCircular[index];
            }
            else
            {
                lb = new LinkButton();
                lb.Text = (index + 1).ToString();
            }
            if ((index + 1) == this.CurrentPageNumber)
            {
                lb2 = new LinkButton();
                lb2.Text = lb.Text;
                lb2.CssClass = this.CssClassPageSelected;
                lb2.Enabled = false;
                lb2.ID = lb.ID;
                lb = lb2;
            }
            else
            {
                lb.Enabled = true;
                lb.CssClass = this.CssClassPageLink;
            }

            return lb;
        }
        #endregion

        #region "Method NavigationOptionContains"
        private bool NavigationOptionContains(NavigationOption obj)
        {
            return ((this.NavigationOptions & obj) == obj);
        }
        #endregion

        #region "Method SeparatorOptionContains"
        private bool SeparatorOptionContains(SeparatorOption obj)
        {
            return ((this.SeparatorOptions & obj) == obj);
        }
        #endregion

    }
}

